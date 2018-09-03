```gherkin
As a developer

I want to show you a simple publish/subscribe solution 
demonstrating how asynchronous command validation and 
execution could be implemented

So that you think I'm awesome
```


The solutions consists of a web site and a worker service.
The web site tells the service to execute commands and subscribes to events published by the worker.
SignalR is used to push those notifications to the end user.


Requirements:
* [Visual Studio 2017](https://visualstudio.microsoft.com/downloads/)
* [RabbitMQ on localhost](https://www.rabbitmq.com/download.html)

[![Build status](https://ci.appveyor.com/api/projects/status/4pc2kc3h5gix06m4?svg=true)](https://ci.appveyor.com/project/henrikbecker/async-validation)
