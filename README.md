# CQRS-Saga-pet-project
CQRS + Saga test project 
The main purpose is: create CQRS application, where queries part could be placed on several replics separately from commands part.
The legend of this project is simple: Author writes a message (saga starts at this moment), message goes for approvement, when message approved, saga is finished. 
Messages passes throuhg commands api, approved messages saves into mongodb, and after that projector makes projection to ElasticSearch where data saves denormalized.
User can watch messages throuhg query api from elasticSearch database.
