# Bot

- This directory contains the bot implementation for the project.
- The bot is designed to interact with the game environment and perform actions based on the game's state.

## Folder Structure
- `Bot/`: Contains the main bot implementation files.
- `Bot/Dispatcher/`: Contains the dispatcher logic that handles incoming game events and routes them to the appropriate bot actions, commands, callbacks, etc.
- `Bot/Commands` : Contains the command definitions that the bot can execute.
- `Bot/Callbacks`: Contains the callback definitions that the bot can respond to.
- `Bot/Attributes`: Contains custom attributes used to annotate bot commands and callbacks, it can be antispam, only private chats, etc.
- `Bot/Tools`: Contains utility classes and methods that assist the bot in its operations, such as logging, configuration management, etc.
- `Bot/Models`: Contains data models used by the bot to represent game entities, player states, etc.
- `Bot/Factory`: Contains factory classes that create instances of bot commands and callbacks, allowing for dynamic instantiation based on the class name.
- 
## Things to consider
- The bot itself uses the `Services` in the root folder to interact with the database.
- The bot is designed to be modular, allowing for easy addition of new commands and callbacks, it can be done just by add NameCmdCommand to add a commando and NameCallbackCallback to add a response to a callback.
- The bot uses a dispatcher to handle incoming events, which allows for efficient routing of commands and callbacks.
