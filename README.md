# CQRS-Saga-pet-project
CQRS + Saga test project 
Цель этого петпрожекта: создать приложение в рамках CQRS подхода, где запросная часть может быть размещена на нескольких репликах и будет отделена от командной части (в большинстве случаев на запросы приходится 90% нагрузки при развёртывании на бою).

Легенда проста: Сообщение поступает от автора (при этом стартует сага), сообщение одобряется, после этого сохраняется в монгу.

Сообщения поступают через CommandsApi после подтверждения сообщения проецирются в ElasticSearch в денормализованном виде, после чего пользователь может их посмотреть через QueriesApi. (mongo и эласти в этом проекте в минимальном виде)

The main purpose is: create CQRS application, where queries part could be placed on several replics separately from commands part.

The legend of this project is simple: Author writes a message (saga starts at this moment), message goes for approvement, when message approved, saga is finished. 

Messages passes throuhg commands api, approved messages saves into mongodb, and after that projector makes projection to ElasticSearch where data saves denormalized.
User can watch messages throuhg query api from elasticSearch database.

![CQRS-Sample](https://user-images.githubusercontent.com/50134408/126861375-6285a227-8169-4feb-8ad8-00ed4e3276bf.jpg)

![CQRSsampleWReplica](https://user-images.githubusercontent.com/50134408/126861696-6d0a7154-970c-4b0a-96e5-ebc69351a953.jpg)
