x <-c(64,23,84,69,83,76,36,65,55,63,62,35,48,51,68,17,50,78,80,56,39,33,87,27,41,37,72,57,24,18,47,21,86,46,54,52,58)

y <-c(2015,2017,1948,1945,1999,1994,2016,1971,2003,1946,2017,1998,1998,1984,1998,1968,1967,2010,1981,1958,1974,1970,1987,1994,1952,1976,1982,1963,1983,1967,2001,1988,1954,2014,1982,1956,2011)

df <- data.frame(age=x,year=y)
View(df)

df



mean(df$age) #Вибіркове Середнє

mean(df$year) #Вибіркове середнє

median(df$age) #медіана
median(df$year)

1/mean(1/df$age) #сер.гармонійне
1/mean(1/df$year)

prod(x)^(1/length(x)) #сер.геометричне
prod(y)^(1/length(y))

var(df$age) #дисперсія
var(df$year)

sd(df$age) #Стандартне відхилення
sd(df$year)

sd(df$age)/mean(df$age)*100  #Коефіцієнт Варіації
sd(df$year)/mean(df$year)*100

quantile(df$age) #П'ятиточкова характеристика
quantile(df$year)

range(df$age) #Макс і мін
range(df$year) 

diff(range(df$age)) #Різниця між макс і мін (Розмах варіації)
diff(range(df$year))

summary(df$age) #Основні характеристики
summary(df$year)

IQR(df$age) #Інтерквартильний розмах
IQR(df$year)

boxplot(df$age,  #коробки з вусами
        main="Age of CarThiefs",
        xlab="Amount of robberies",
        ylab="Age",
        col="orange",
        border="black",
        horizontal = TRUE,
        notch = TRUE)
boxplot(df$year, 
        main="Manufacture Year of Cars",
        xlab="Year",
        ylab="Amount",
        col="blue",
        border="brown",
        horizontal = TRUE,
        notch = TRUE)

hist(df$age)
hist(df$year)

install.packages("moments") #Встановлення бібліотечки
library(moments) #Використовуємо бібліотеку

skewness(df$age) #Коефіцієнт асиметрії
skewness(df$year)

kurtosis(df$age)  #Коефіцієнт ексцесу
kurtosis(df$year)

quantile(df$age,probs = c(0.1,0.9)) #1 та 9 децилі
quantile(df$year,probs = c(0.1,0.9))

