# EventDispatcher

A simple system for dispatching events.
Allows you to explicitly set the type of message to send / receive, so that the recipient receives only the data it needs.
It's very simple in use. Just inherit your own class from an abstract generic base receiver where generic parameter is the message type you want this receiver to react and implement the only abstract method which will be called when any message of defined type will be dispatched.
