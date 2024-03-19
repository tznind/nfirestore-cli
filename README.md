# nfirestore-cli

This is a dotnet tui for interacting with Firestore databases.  

It is based on [firestore-cli](https://github.com/Ion-manden/firestore-cli/tree/main) a Go application with similar remit.

Expanded functionality includes

- Support for nested collections

![firestore-tui](https://github.com/tznind/nfirestore-cli/assets/31306100/40e1737e-0dac-491a-ba4f-e6967e6ceb09)

## Running Program
Enter the project root and run

```
docker-compose up
export FIRESTORE_EMULATOR_HOST=localhost:8200
dotnet run --project ./nfirestore-cli/nfirestore-cli.csproj -- -p dummy_project
```

If using Powershell instead of linux, set your env var as follows:
```
$Env:FIRESTORE_EMULATOR_HOST = 'localhost:8200'
```
