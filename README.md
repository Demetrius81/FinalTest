# Финальный тест
### Задание

>Организуйте систему учёта для питомника, в котором живут домашние и вьючные животные.

### Решение

Была выбрана следующая схема организации системы учета питомцев:
 * База данных для удаленного хранения моделей
 * API сервис для передачи данных от базы данных приложению
 * Клиентская библиотека для работы с API сервисом и унификации с разными видами приложений
 * WPF приложение, выполненное по шаблону MVVM с управлением сервисами внутри приложения при помощи системы внедрения зависимостей
 * Для упрощения дальнейшей эксплуатации программного продукта было решено придерживаться принципов SOLID

Далее была разработана диаграмма доменной модели и диаграмма связи сущностей.

###### ER диаграмма

![ER диаграмма](https://github.com/Demetrius81/FinalTest/blob/dev/Diagrams/ERD_Diagram.jpg)

###### Диаграмма классов доменной модели

![Диаграмма классов доменной модели](https://github.com/Demetrius81/FinalTest/blob/dev/Diagrams/DomainModelDiagram.jpg)

Для хранения данных была выбрана база данных SQLite.  
Для взаимодействия базы данных с доменной моделью программы была выбрана  
ОРМ Entity Framework Core 7.  
Для написания API сервиса была выбрана платформа ASP.NET.  
Была создана библиотека базовых интерфейсов, которые описывают основные сущности программы.  
Была создана библиотека доменных моделей.  
Был создан API сервис с использованием принципа инверсии зависимостей, реализующий базовый CRUD функционал.  
Далее была создана клиентская библиотека.  
Связка API сервис - клиентская библиотека была протестирована и отлажена в консольном приложении.  
Была создана UserCase диаграмма пользования приложением.  

###### User Case диаграмма

![UserCase диаграмма](https://github.com/Demetrius81/FinalTest/blob/dev/Diagrams/UseCaseDiagram.jpg)

Далее на основе User case диаграммы был разработан интерфейс пользователя.  
Был реализован WPF проект. В проекте использован шаблон MVVM. Вся логика отделена от представлений.  
Также в приложение был внедрен DependencyInjection контейтер и организовано внедрение нужных сервисов на основе их интерфейсов.  

###### Диаграмма классов WPF приложения

![Диаграмма классов WPF приложения](https://github.com/Demetrius81/FinalTest/blob/dev/Diagrams/AppWpfClassDiagram.jpg)
