# Программное средство автоматизации процессов службы поддержки клиентов компании
- [C4 component level diagram](https://github.com/Strine-Vll/SupportService/blob/main/diagrams/C4_component.png)
- [C4 container level diagram](https://github.com/Strine-Vll/SupportService/blob/main/diagrams/C4_container.png)
- [Design](https://github.com/Strine-Vll/SupportService/blob/main/diagrams/design.png)
- [Class diagram](https://github.com/Strine-Vll/SupportService/blob/main/diagrams/class_diagram.png)
- [ERD](https://github.com/Strine-Vll/SupportService/blob/main/diagrams/erd.PNG)
### User-flow
- [Пользователь](https://github.com/Strine-Vll/SupportService/blob/main/diagrams/user-user-flow.PNG)
- [Специалист службы поддержки](https://github.com/Strine-Vll/SupportService/blob/main/diagrams/sup-user-flow.PNG)
- [Менеджер](https://github.com/Strine-Vll/SupportService/blob/main/diagrams/manager-user-flow.PNG)
### Архитектура
- [use-case](https://github.com/Strine-Vll/SupportService/blob/main/diagrams/use-case.png)
- [Диаграмма состояний](https://github.com/Strine-Vll/SupportService/blob/main/diagrams/state-diagram.png)
- [Диаграмма последовательности](https://github.com/Strine-Vll/SupportService/blob/main/diagrams/sequence-diagram.png)
- [Диаграмма развёртывания](https://github.com/Strine-Vll/SupportService/blob/main/diagrams/deployment-diagram.png)
- [Схема алгоритма](https://github.com/Strine-Vll/SupportService/blob/main/diagrams/algorithm.png)
### Документация
[Документация к разработанному API](https://github.com/Strine-Vll/SupportService/blob/main/miscellaneous/swagger.json)
### Оценка качества кода
![Рисунок 1](https://github.com/Strine-Vll/SupportService/blob/main/miscellaneous/metrics1.png)
![Рисунок 2](https://github.com/Strine-Vll/SupportService/blob/main/miscellaneous/metrics2.png)
### Тестирование
![Tests Result](https://github.com/Strine-Vll/SupportService/blob/main/miscellaneous/testsResult.PNG)

Для запуска интеграционных тестов требуется докер (используется библиотека testcontainers)
### Пользовательский интерфейс
- Примеры экранов UI
![Рисунок 1](https://github.com/Strine-Vll/SupportService/blob/main/miscellaneous/auth.PNG)
![Рисунок 1](https://github.com/Strine-Vll/SupportService/blob/main/miscellaneous/auth-valid.PNG)
![Рисунок 1](https://github.com/Strine-Vll/SupportService/blob/main/miscellaneous/create-request.PNG)
![Рисунок 1](https://github.com/Strine-Vll/SupportService/blob/main/miscellaneous/groups.PNG)
![Рисунок 1](https://github.com/Strine-Vll/SupportService/blob/main/miscellaneous/register.PNG)
![Рисунок 1](https://github.com/Strine-Vll/SupportService/blob/main/miscellaneous/request.PNG)
![Рисунок 1](https://github.com/Strine-Vll/SupportService/blob/main/miscellaneous/requests.PNG)
### Безопасность
- JWT-токены
- RBAC
- CORS
- Password hash и Salt
### Развёртывание
Убедитесь, что IIS установлен на вашем сервере Windows. Для этого откройте "Панель управления", затем "Программы и компоненты", затем "Включение или отключение компонентов Windows" и выберите "Internet Information Services". Убедитесь, что установлены необходимые компоненты, такие как ASP.NET и другие модули, необходимые для работы вашего приложения. 
Создание нового сайта: 
- откройте диспетчер IIS;
- щелкните правой кнопкой мыши на "Сайты" и выберите "Добавить веб-сайт";
- укажите имя сайта, физический путь к папке с опубликованным приложением и порт (5222 или 7239 для HTTPS). Нажмите "ОК", чтобы создать сайт. 
- выберите созданный сайт в диспетчере IIS и перейдите в раздел "Настройки приложения" и убедитесь, что выбран правильный пул приложений с поддержкой .NET;
- после настройки запустите сайт из диспетчера IIS. 

Для развёртывания Angular-приложения требуется выполнить следующие действия:
- перейдите в корневую папку вашего Angular-проекта и выполните команду для сборки приложения: ng build --prod --base-href /ваш_путь/
Это создаст папку dist, содержащую все необходимые файлы для развертывания.
- после создания сайта щелкните правой кнопкой мыши на вашем сайте и выберите "Добавить приложение". Укажите алиас и физический путь к папке с опубликованными файлами;
- откройте браузер и перейдите по адресу вашего сайта, чтобы убедиться, что приложение загружается и работает корректно.