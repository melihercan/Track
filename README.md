WORK IN PROGRESS...

# Track

![alt text](https://github.com/melihercan/Track/blob/master/Track.png)

Sample project to demonstrate the usage of the following technologies:
- Clean Architecture
- DotNetCore
- Blazor
- Xamarin
- SignalR
- WebAPI
- MediatR

The system will keep track of real time status of various devices such as Drones, Cars, Planes, Ships etc. Tracked devices will be sending device data to the backend server and the server will stream the data to Web (Blazor), Mobile Phone (Xamarin) or Console (DotNetCore) clients.

Device data will contain the following information:
- Id
- GroupId
- Timestamp
- Latitude
- Longitude
- Altitude
- Speed

### Core Layer

This is the core of the application. It contains:
- Entities
- Use Cases
- Interfaces


### Infrastructure

Implementation of the interfaces:
- Device
- Repositrory
- User

### UserInterfaces

This layer presents user interfaces:
- Web UI
- Mobile UI
- Console UI

## License

This project is licensed with the [MIT license](LICENSE).


