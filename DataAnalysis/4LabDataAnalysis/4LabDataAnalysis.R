edcTt <- read.csv("https://raw.githubusercontent.com/vincentarelbundock/Rdatasets/master/csv/MASS/road.csv", stringsAsFactors = FALSE)
edcT <- (edcTt[,2:length(edcTt)])
View(edcT)
#A data frame with the annual deaths in road accidents for half the US states.
#state - name.

#deaths - number of deaths.

#drivers -number of drivers (in 10,000s).

#popden -population density in people per square mile.

#rural -length of rural roads, in 1000s of miles.

#temp -average daily maximum temperature in January.

#fuel - fuel consumption in 10,000,000 US gallons per year.

# 1.Для двох (трьох) масивів взаємопов’язаних даних 
#(з попередньої роботи) побудувати діаграму розсіювання, визначити тип залежності. 

plot(edcT$deaths~edcT$drivers,
     xlab="Drivers", ylab="Deaths") #Діаграма розсіювання для deaths and drivers

abline(lm(edcT$deaths~edcT$drivers),col="red")  # Пiдгонка прямою

plot(edcT$popden~edcT$drivers,
     xlab="Population Density", ylab="Drivers") #Діаграма розсіювання 

abline(lm(edcT$popden~edcT$drivers),col="red")  # Пiдгонка прямою

plot(edcT$drivers~edcT$fuel,
      xlab="Drivers", ylab="Cost of Fuel") #Діаграма розсіювання 
abline(lm(edcT$drivers~edcT$fuel),col="red")  # Пiдгонка прямою

#2. Порахувати параметри регресійної моделі.

#обрахування параметрів рівняння регресії
lm.edcT <- lm(formula= edcT$drivers~edcT$deaths,data=edcT) #відгук drivers a регресор - fuel
lm.edcT$coefficients
plot(edcT$drivers,edcT$deaths) #Діаграма розсіювання
bls1<- lm(edcT$drivers~edcT$deaths,data=edcT)$coefficients
bls1
abline(lm(edcT$drivers~edcT$deaths),col="red")
segments(edcT$drivers,edcT$deaths,edcT$drivers,bls1[1]+bls1[2]*edcT$deaths)

#Зараз скажемо
#лише, що Age~ztop — це формула, яка вказує, що вiдгуком у нашiй
#моделi є Age, а регресором — ztop; опцiя data вказує фрейм, з якого
#беруться данi для пiдгонки. Результатом виконання функцiї lm() є об’єкт
#складної структури, МНК-оцiнки коефiцiєнтiв регресiї мiстяться у ньому в
#атрибутi $coefficients.


#Вивід модельних значень
a0<- lm.edcT$coefficients[1]
a1<-lm.edcT$coefficients[2]
xmin<-min(edcT$deaths)
xmax<-max(edcT$deaths)
x<-seq(from=xmin,to=xmax,length.out = 100)
y<-a0+a1*x

#Вивід графіка залежності
plot(edcT$deaths,edcT$drivers, main="",xlab="Top Depth",ylab="Age")
grid()
lines(x,y,col="red")

summary(lm.edcT)

#Горизонтальна Регресія
par(mfrow = c(1,2))
als2<- lm(log(deaths)~log(drivers),data=edcT)$coefficients
summary(als2)
1#Діаграма розсіювання
plot(log(edcT$deaths),log(edcT$drivers), 
     xlab="Deaths", ylab="Drivers",sub="(a)")
abline(bls1,col="blue") # bls1 з попереднього скрипту
abline(c(-als2[1]/als2[2],1/als2[2]),col="red")

segments(log(edcT$deaths),log(edcT$drivers),
         + als2[1]+als2[2]*log(edcT$drivers),log(edcT$drivers))

#Ортогональна Регресія

#Тут для пiдрахунку коефiцiєнтiв ортогональної регресiї використана
#функцiя Deming() з бiблiотеки MethComp. Перший її параметр — це регресор
#(горизонтальна координата), а другий — вiдгук (вертикальна координата).
#Звернiть увагу на те, що при вiдображеннi рисунку функцiєю plot() встановлена опцiя asp=1, яка задає однаковий масштаб як по горизонталi, так i
#по вертикалi. Iнакше вiдрiзки, що з’єднують точки даних з їх проекцiями на
#рисунку не виглядали б перпендикулярними до лiнiї регресiї.

library(MethComp)

tls<-Deming(edcT$deaths,edcT$drivers)

x<-edcT$deaths
y<-edcT$drivers
b0<-tls[1]
b1<-tls[2]

# розраховуємо координати проекцiй даних на пряму регресiї:

x0<-(x+b1*y-b0*b1)/(1+b1^2)
y0<-b0+b1*x0

plot(edcT$deaths,edcT$drivers,asp=1,
      xlim=c(155,1000),ylim=c(90,400),sub="(b)")
segments(x,y,x0,y0)
abline(tls[1:2],col="green")

abline(bls1,col="blue")

abline(c(-als2[1]/als2[2],1/als2[2]),col="red")

#Регресiя ваги та зросту актрис: а) “обернена” регресiя b) ортогональна
#регресiя. Лiнiя регресiї: синiм — звичайний МНК, червоним — “обернена”,
#зеленим — ортогональна.

#Квантильна Регресія
par(mfrow = c(1,1))
install.packages("quantreg")
library("quantreg")
plot(edcT$deaths,edcT$drivers,xlim=c(155,1000),
      xlab="height",ylab="weight")
lmed<-rq(drivers~deaths,tau=0.5,data=edcT)$coefficients
abline(lmed)

lq10<-rq(drivers~deaths,tau=0.1,data=edcT)$coefficients
abline(lq10,col="red")

actr10<-edcT[edcT$deaths<lq10[1]+lq10[2]*edcT$drivers,]
text(edcT$drivers,edcT$deaths,
      labels=actr10$name,pos=4,col="red")

lq90<-rq(drivers~deaths,tau=0.9,data=edcT)$coefficients
abline(lq90,col="blue")

actr90<-edcT[edcT$drivers>lq90[1]+lq90[2]*edcT$deaths,]
text(actr90$drivers,actr90$deaths,
      labels=actr90$name,pos=4,col="blue")

#install.packages("car")
library(car)
scatterplotMatrix(edcT,diagonal="histogram",smoother=F)

model<- lm(edcT$drivers~edcT$deaths+edcT$fuel,data=edcT)
summary(model)
#дiаграма прогноз вiдгук
plot(model$fitted.values,edcT$drivers,
     xlab="Drivers Forecast",ylab="True drivers")
abline(c(0,1),col="red")

#Щоб помiтити такi нелiнiйнi ефекти краще користуватись дiаграмою
#прогноз-залишки:
plot(model$fitted.values,model$residuals,
      xlab="prediction",ylab="residuals") 

abline(0,0,col="red")
#Побудуємо QQ-дiаграму для перевiрки
#нормальностi розподiлу похибок: 

qqnorm(model$residuals)
qqline(model$residuals,col="red")

#8. 
model2 <- lm(drivers~deaths+fuel+I(deaths*fuel)^2, data = edcT)
summary(model2)

#Графічний аналі ззалишків
plot(model2$residuals~model2$fitted.values)
abline(0,0,col="red")
qqnorm(model2$residuals)
qqline(model2$residuals,col="red")

