# nfirestore-cli

This is a dotnet tui for interacting with Firestore databases.  

It is based on [firestore-cli](https://github.com/Ion-manden/firestore-cli/tree/main) a Go application with similar remit.

Expanded functionality includes

- Support for nested collections
- Configurable query limit (defaults to 100)
  
![firestore-tui2](https://github.com/tznind/nfirestore-cli/assets/31306100/e34902c2-0830-4f63-959c-64171142c7bf)

## Running Program
Enter the project root and start the firestore emulator:

```
docker-compose up
```

Build and run the program

```
cd .\nfirestore-cli\
dotnet run
```

