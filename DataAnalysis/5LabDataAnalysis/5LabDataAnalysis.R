d<- read.csv("https://raw.githubusercontent.com/antomys/5LabDataAnalysis/master/ratfeed.csv?token=AMCAJYFFCAAHS3YENBGXWZ25ZQOSQ",
                stringsAsFactors = FALSE)
d1<-read.csv("https://raw.githubusercontent.com/antomys/5LabDataAnalysis/master/ratfeed.csv?token=AMCAJYFFCAAHS3YENBGXWZ25ZQOSQ",
             stringsAsFactors = FALSE)
d
WeightAmount <-(d[,c(1,2)])
WeightAmount
WeightType <-(d[,c(1,3)])
WeightType
#Factorization -----------------
d$Diet.Amount <- as.factor(d$Diet.Amount)
d$Diet.Type <- as.factor(d$Diet.Type)
WeightAmount$Diet.Amount <- as.factor(WeightAmount$Diet.Amount)
WeightType$Diet.Type <- as.factor(WeightType$Diet.Type)
str(d)
str(WeightType)
str(WeightAmount)
#RATFEED

#1. Для трьох (або більше) градацій якісної змінної вивести у виді 
#лінійних діаграм значення по кожній з градацій та значення 
#середніх (різними кольорами та з підписами).

#Average----------------------------------------
MeanA <-tapply(WeightAmount$Weight.Gain,WeightAmount$Diet.Amount,mean)
MeanTP <-tapply(WeightType$Weight.Gain,WeightType$Diet.Type,mean)
#-----------------------------------------------

#Graphics ------------------------------------------------------------------------
stripchart(WeightAmount$Weight.Gain~WeightAmount$Diet.Amount,pch = 19,
           col = c("blue", "red"))

stripchart(WeightType$Weight.Gain~WeightType$Diet.Type,pch = 19,
           col = c("blue", "red", "black"))

#Малюємо із середніми ----------------------------------------------------

Means <- data.frame( Weight.Gain= as.numeric(tapply(WeightType$Weight.Gain,
                                                    WeightType$Diet.Type, mean)),
                    Diet.Type = rep("Means", 3))
data <- rbind(WeightType,Means)

stripchart(data$Weight.Gain ~ data$Diet.Type, pch = 19,
           col = c("blue", "red", "black"))
points(x = Means$Weight.Gain, y = c(4, 4, 4), pch = 19,
       col = c("blue", "red", "black"))

Means2 <- data.frame( Weight.Gain= as.numeric(tapply(WeightAmount$Weight.Gain,
                                                     WeightAmount$Diet.Amount, mean)),
                     Diet.Amount = rep("Means", 2))
data2 <- rbind(WeightAmount,Means2)

stripchart(data2$Weight.Gain ~ data2$Diet.Amount, pch = 19,
           col = c("blue", "red", "black"))

#-------------------------------------------------------
#---------------------------------------------------------------------------------
#2. Провести дисперсійний аналіз для цих даних. 
#ONE-WAY NORMAL ------------------------------
summary(d)

anova.res=aov(Diet.Amount~Diet.Type, data=d1)
summary(anova.res)
#---------------------------------------
#Краскела-Уоллиса ---------------------
kruskal.test(Diet.Amount~Diet.Type,data=d)
#Розвідувальний Аналіз--------------------------------
library(ggplot2)

ggplot(data = d, aes(x = Diet.Type, y = Weight.Gain)) + 
  geom_boxplot(aes(fill = Diet.Amount))

#Описові статистики-----------------------------
#install.packages("doBy")
require(doBy)
summaryBy(Weight.Gain~Diet.Type+Diet.Amount,data=d,
          FUN=c(mean,sd,length))

#Графік дизайна експеримента--------------------
plot.design(d)
#Из полученного графика видно, 
#что наибольшая разница в средних приростах 
#веса крыс связана с уровнем содержания белка в корме, 
#тогда как эффект источника происхождения белка выражен 
#в меньшей степени.

#Графік взаємодії------------------
with(d, interaction.plot(x.factor=Diet.Amount,
                         trace.factor = Diet.Type,
                         response = Weight.Gain))
#Из приведенного рисунка видно, 
#что при высоком содержании белка в корме, 
#прирост веса крыс в среднем также высок, 
#но при условии, что этот белок имеет животное 
#происхождение. Если же содержание белка низкое, 
#то ситуация меняется на противоположную - прирост 
#оказывается несколько выше (хотя и не намного) в группе 
#крыс, получавших корм растительного происхождения.

#Дисперсійний аналіз через lm()------------------
attach(d)
lm.res <-lm(Weight.Gain ~ Diet.Type*Diet.Amount)
summary(lm.res)

#У вигляді АНОВА таблиці:
anova(lm.res)

#3. Провести аналіз контрастів (якщо є потреба).
#Контрасты комбинаций условий (treatment contrasts)------------------
boxplot(Weight.Gain~Diet.Type, data=WeightType,
        xlab="Тип дієти",ylab="Прибавка у вазі",
        col="coral")
#Функция contrasts() позволяет ознакомиться 
#с матрицей контрастов для того или иного фактора:
contrasts(d$Diet.Type)

#Контрасты сумм (sum contrasts)----------------------
contrasts(d$Diet.Type)<-contr.sum(n=3)
contrasts(d$Diet.Type)
#Как видим, в отличие от матрицы с контрастами комбинаций условий, суммы 
#по всем столбцам матрицы контрастов сумм равны нулю.

#Выполнив дисперсионный анализ, увидим, 
#что интерпретация оцененных параметров модели 
#теперь также изменилась:
lm3.res<- lm(Weight.Gain~Diet.Type,data=d)
summary(lm3.res)
#Первая строка в таблице с результатами анализа - (Intercept) - 
#содержит среднее значение набранного веса

with(d, mean(tapply(Weight.Gain, Diet.Type, mean)))

#Контрасты Хелмерта --------------------------------
contrasts(d$Diet.Type) <- contr.helmert(n = 3)
contrasts(d$Diet.Type)
#Обратите внимание: суммы по всем столбцам матрицы контрастов Хелмерта равны нулю. Кроме того, сумма произведений элементов 
#любых двух столбцов матрицы также равна нулю. Например:
mat <- contrasts(d$Diet.Type)

sum(mat[, 1]*mat[, 2])
#Контрасты с такими свойствами называются ортогональными.

#Рассчитаем теперь параметры новой линейной модели:

lm.res4 <- lm(Weight.Gain ~ Diet.Type, data = d)

summary(lm.res4)
#Следует отметить, что несмотря на свои привлекательные 
#математические свойства (ортогональность), контрасты 
#Хелмерта редко применяются на практике поскольку 
#получаемые параметры модели сложно интерпретировать 
#в приложении к конкретным проблемам.

#4. За результатами аналізу зробити висновки.

