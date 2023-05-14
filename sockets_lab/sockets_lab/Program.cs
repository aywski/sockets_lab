using sockets_lab;

Console.WriteLine("server or client? (S/C): ");
string who = Console.ReadLine();

switch (who.ToLower())
{
    case "c":
        bool exit = false;
        Console.WriteLine("\"Help\" for info\nType the command: ");
        while (!exit)
        {
            Console.Write("// ");
            string control = Console.ReadLine();
            if (control.ToLower() == "exit")
                exit = true;
            else Client.Connect("127.0.0.1", control.ToLower());
        }
        break;
    case "s":
        Server.RunServer();
        break;
    default:
        break;
}
