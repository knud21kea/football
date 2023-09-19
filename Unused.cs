// ---------------------------------------------------------------------------
// simple CLI

/* bool menu = true;

        while (menu)
        {
            Console.Clear();
            Console.WriteLine("-- Football standings App --");
            Console.WriteLine("1. " + leagues[0].Name);
            Console.WriteLine("2. " + leagues[1].Name);
            Console.WriteLine("3. " + leagues[2].Name);
            Console.WriteLine("0. Exit");

            Console.Write("Please enter your choice (1, 2, 3, or 0 to exit): ");
            string? userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    DataHandler.JustPlayingAround(leagues[0]);
                    break;

                case "2":
                    DataHandler.JustPlayingAround(leagues[1]);
                    break;

                case "3":
                    DataHandler.JustPlayingAround(leagues[2]);
                    break;

                case "0":
                    menu = false;
                    Console.WriteLine("Exiting the program. Press any key to continue...");
                    break;

                default:
                    Console.WriteLine("Invalid input. Please enter 1, 2, 3, or 0 to exit. Press any key to continue...");
                    Console.ReadKey();
                    break;
            }

            /* if (menu)
            {
                Console.WriteLine("Press any key to return to the menu...");
                Console.ReadKey();
            }
        } */


        // -----------------------------------------------------------------------