# nfirestore-cli

This is a dotnet tui for interacting with Firestore databases.  

It is based on [firestore-cli](https://github.com/Ion-manden/firestore-cli/tree/main) a Go application with similar remit.

Expanded functionality includes

- Support for nested collections
- Configurable query limit (defaults to 100)
  
![demo gif](https://github.com/tznind/nfirestore-cli/assets/31306100/e34902c2-0830-4f63-959c-64171142c7bf)

## Install and Run

Install the tool by running

```
dotnet tool install --global nfirestore-cli
```

You can then run with

```
nfirestore-cli --help
```

Supports connecting to an emulator (e.g. a docker container running [mtlynch/firestore-emulator](https://github.com/mtlynch/firestore-emulator-docker)) by using the `-e` parameter:

```
nfirestore-cli -e localhost:8200 -p test
```

To connect to Google Cloud you will need to run gcloud auth - see [Google Docs for full details](https://cloud.google.com/docs/authentication/provide-credentials-adc#how-to).  For example:

```
gcloud auth application-default login
nfirestore-cli -p my-project
gcloud auth application-default revoke
```

## Update/Remove

You can update to the latest version using

```
dotnet tool update --global nfirestore-cli
```

You can uninstall using 

```
dotnet tool uninstall --global nfirestore-cli
```

## Development

Enter the project root and start the firestore emulator:

```
docker-compose up
```

Build and run the program

```
dotnet run
```

