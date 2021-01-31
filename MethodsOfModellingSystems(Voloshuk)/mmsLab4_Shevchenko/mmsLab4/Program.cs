using System;
using System.Collections.Generic;

namespace mmsLab4
{
    class Program
    {
        //вхідні дані
        const double a = 0.2;
        const double m1 = 0.1;
        const double m2 = 0.2;
        const double Tmod = 1000;
        const int Qmax = 6; //максимальна довжина черги
        static void Main(string[] args)
        {
            Timer time = new Timer(Tmod, a);
            Generator gen = new Generator(a, 0); //вхідний потік вимог
            Queue queue = new Queue(Qmax, 1);
            Chanel K11 = new Chanel(m1, 2);
            Chanel K12 = new Chanel(m1, 3);
            Chanel K2 = new Chanel(m2, 4);
            List<Vimoga> vl = new List<Vimoga>();//усі вимоги, які є в мережі
            Statistic stat = new Statistic();//збір статистики

            while (time.Next())//поки не вийшов час
            {
                vl.Add(gen.GenerateNext());//генерація вимоги

                
                while (vl.Count > 0)//доки є вимоги в мережі (які мали змінити стан до теперішнього часу)
                {
                    //пошук вимоги, яка має змінити стан раніше (при однаковому часі обирається з ближчим до виходу станом)
                    int min = 0;
                    for (int i = 1; i < vl.Count; ++i)
                    {
                        if (vl[i].timeNext < vl[min].timeNext)
                        {
                            min = i;
                            continue;
                        }
                        if (vl[i].timeNext == vl[min].timeNext)
                        {
                            if(vl[i].status > vl[min].status)
                            {
                                min = i;
                            }
                        }
                    }
                    if (vl[min].timeNext > time.curTime) break;

                    //передаю теперішній стан ММО в для збору статистики
                    stat.Next(vl[min].timeNext, queue.Ncur, K11.Enabled, K12.Enabled, K2.Enabled);

                    //у залежності від стану вимоги намагаюся перемістити її в наступний стан
                    switch (vl[min].status)
                    {
                        case 0://якщо тільки створена
                            if (K11.Enabled)//намагаюся помістити в пристрій К11
                            {
                                vl[min].ChangeStatus(K11.GetTime(), K11.id);
                                K11.timeNext = vl[min].timeNext;
                                K11.Enabled = false;
                            }
                            else
                            {//якщо К11 зайнятий, то намагаюся помістити в пристрій К12
                                if (K12.Enabled)
                                {
                                    vl[min].ChangeStatus(K12.GetTime(), K12.id);
                                    K12.timeNext = vl[min].timeNext;
                                    K12.Enabled = false;
                                }
                                else
                                {//якщо всі пристрої СМО1 зайняті, то намагаюся помістити в чергу
                                    if (queue.Enabled)
                                    {
                                        vl[min].timeNext = Math.Min(K11.timeNext, K12.timeNext);
                                        vl[min].status = queue.id;
                                        queue.Add();
                                    }
                                    else
                                    {//інакше вимога не може бути обслугована
                                        vl.RemoveAt(min);
                                        stat.UnServ();
                                    }
                                }
                            }
                            break;
                        case 1://якщо вимога знаходиться в черзі
                            if (K11.Enabled)//намагаюся помістити в пристрій К11
                            {
                                vl[min].ChangeStatus(K11.GetTime(), K11.id);
                                K11.timeNext = vl[min].timeNext;
                                K11.Enabled = false;
                                queue.Remove();
                            }
                            else
                            {//якщо К11 зайнятий, то намагаюся помістити в пристрій К12
                                if (K12.Enabled)
                                {
                                    vl[min].ChangeStatus(K12.GetTime(), K12.id);
                                    K12.timeNext = vl[min].timeNext;
                                    K12.Enabled = false;
                                    queue.Remove();
                                }
                                else
                                {//інакше залишаю в черзі
                                    vl[min].timeNext = Math.Min(K11.timeNext, K12.timeNext);
                                }
                            }
                            break;
                        case 2://якщо вимога в пристрої К11
                            if (K2.Enabled)//намагаюся помістити в пристрій К2
                            {
                                vl[min].ChangeStatus(K2.GetTime(), K2.id);
                                K2.timeNext = vl[min].timeNext;
                                K11.Enabled = true;
                                K2.Enabled = false;
                            }
                            else
                            {//інакше виконується блокування й вимога залишається в притрої К11
                                vl[min].timeNext = K11.timeNext = K2.timeNext;
                            }
                            break;
                        case 3://якщо вимога в пристрої К12
                            if (K2.Enabled)//намагаюся помістити в пристрій К2
                            {
                                vl[min].ChangeStatus(K2.GetTime(), K2.id);
                                K2.timeNext = vl[min].timeNext;
                                K12.Enabled = true;
                                K2.Enabled = false;
                            }
                            else
                            {//інакше виконується блокування й вимога залишається в притрої К11
                                vl[min].timeNext = K12.timeNext = K2.timeNext;
                            }
                            break;
                        case 4://якщо вимога в пристрої К2, то вона обслугована ММО
                            stat.Serv(vl[min].timeNext - vl[min].timeStart);
                            K2.Enabled = true;
                            vl.RemoveAt(min);
                            break;
                    }
                }
            }

            stat.Results(Tmod);
        }
    }
}
