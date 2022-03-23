using SingletonBehavior;

Console.WriteLine("###### SINGLETON BEHAVIOUR ######");

CreateInstanceBehavior();
RunMethodsWithLockBehavior();
RunMethodsBehavior();
Finish();

static void Finish()
{
    Console.WriteLine("\n\n\nDone...");
    Console.ReadLine();
}

static void CreateInstanceBehavior()
{
    Console.WriteLine("\n\n# GET SINGLETON INSTANCE - MULTITHREADED ENVIRONMENT");

    Console.WriteLine(
        "\n{0}\n{1}\n\n{2}\n",
        "If you see the same value, then singleton was reused (yay!)",
        "If you see different values, then 2 singletons were created (booo!!)",
        "RESULT:"
        );

    Thread process1 = new(() =>
    {
        GetSingletonInstance("FOO");
    });
    Thread process2 = new(() =>
    {
        GetSingletonInstance("BAR");
    });

    process1.Start();
    process2.Start();

    process1.Join();
    process2.Join();
}

static void GetSingletonInstance(string value)
{
    Singleton singleton = Singleton.GetInstance(value);
    Console.WriteLine(singleton.Value);
}

static void RunMethodsWithLockBehavior()
{
    Console.WriteLine("\n\n# RUN METHODS WITH LOCK - MULTITHREADED ENVIRONMENT");
    Console.WriteLine("\nThe execution of one method must wait for the execution of the other.\n\n");

    Thread process1 = new(() =>
    {
        Singleton.GetInstance("FOO").LongRunWithLock(executionId: 1, millisecondsDelay: 2000);
    });

    Thread process2 = new(() =>
    {
        Singleton.GetInstance("BAR").LongRunWithLock(executionId: 2, millisecondsDelay: 2000);
    });

    process1.Start();
    process2.Start();

    process1.Join();
    process2.Join();
}


static void RunMethodsBehavior()
{
    Console.WriteLine("\n\n# RUN METHODS - MULTITHREADED ENVIRONMENT");

    Thread process1 = new(() =>
    {
        Singleton.GetInstance("FOO").LongRun(executionId: 1, millisecondsDelay: 2000);
    });

    Thread process2 = new(() =>
    {
        Singleton.GetInstance("BAR").LongRun(executionId: 2, millisecondsDelay: 2000);
    });

    process1.Start();
    process2.Start();

    process1.Join();
    process2.Join();
}