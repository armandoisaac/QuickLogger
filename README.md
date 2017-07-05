# QuickLogger

Welcome to the QuickLogger project. This project has been built to add logging functionality to your application in an easy and simple way. This project allows you to output your logs to the Console or a File with a simple configuration. You will also be able to create your own storage using the ILogStorage interface.

# How to use this package
### Installing the nuget package

The QuickLogger project has it's own package that can be downloaded and installed from your project using the Nuget package.

To install QuickLogger, run the following command in the Package Manager Console:

    PM> Install-Package QuickLogger

### Creating an instance of the logger

The **LoggerBuilder** class allows you to create all the required configuration that the Logger class needs in order to work properly.

    var logBuilder = new LoggerBuilder()

Using the Builder Pattern, you will be able to pass all the configuration options to the builder before creating the client.

    // Enables all log types for storage
    logBuilder.WithAllLogTypes();
    
    // Defines which log types will be stored
    logBuilder.WithLogTypes(LogType types);
    
    // Defines where the log messages will be stored
    logBuilder.WithStorage(params ILogStorage[] output);
    
    // Swallows all exceptions when storing the log messages
    logBuilder.WithFireAndForget();
    
    // Allows all exceptions on the storage providers to be sent to the client.
    logBuilder.WithExceptions()

Once configured, you will be able to build the Logger. This will return a Logger object.

    var logger = logBuilder.Build();

### Adding a message to the log

The **QuickLogger** package allows you to store five different types of log: 

- **Debug:** Messages that are only logged for debugging purposes
- **Info:** Messages that are logged for information that may be useful
- **Warn:** Indicate a potential problem, though it does not immediately impact the system
- **Error:** Indicate an exception occurred, disrupting some thread or aspect of the system, but leaving the overall system
- **Fatal:** Indicate an error has caused an entire sub process or application to stop.
- **Other:** Any custom log type

The Logger class contains several methods, as well method overrides that allows you to store any kind of information.

    logger.Debug("This is a random message");
    
    logger.Info("This is an info message.", new Dictionary<string, object>
    {
    	{"UsefullInfo", "Some value"}
    });
    
    logger.Warn("Warning message", new Dictionary<string, object>
    {
    	{"WarningDebugInfo", "Some value"}
    });
    
    logger.Error("Error message", new Exception());
    
    logger.Fatal("Fatal message", new FatalException());

### Using the ConsoleStorage

The **ConsoleStorage** class allows you to output your log messages to the console. The messages will display a different foreground color depending on the type of the log. You can use the LoggerBuilder.WithStorage method to add the ConsoleStorage as an storage for your messages.

     var logBuilder = new LoggerBuilder()
    				  .WithStorage(new ConsoleStorage());

### Using the FileStorage

The **FileStorage** class allows you to output your log messages to a file. You can also configure the FileStorage to set a max file size for the file and the path where you want to store the log file. 

You can use the LoggerBuilder.WithStorage method to add the FileStorage as an storage for your messages.
    
    var logBuilder = new LoggerBuilder()
					    .WithStorage(
							new FileStorageBuilder()
							    .WithStoragePath(@"C:\logs\")	// Where is the file going to be stored
							    .WithFilePrefix("log")			// The prefix of the file. The file name pattern is {prefix}_{date:yyyy-MM-dd}.log 
							    .WithMaxFileSize(10)
							    .Build()
						);

### Creating your own Storage

You can create your own storage method by using the **ILogStorage** interface. When implementing this interface, you will be able to pass your object to the LoggerBuilder.WithStorage method.
