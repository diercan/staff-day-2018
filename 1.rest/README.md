# REST—a guide to (kind of) getting it
This project is built as a starting point for the [REST—a guide to (kind of) getting it](slides.pdf) presentation. The API breaks the _hypermedia as the engine of state_ constraint of REST by design (the presentation covers how to transform such an API into a RESTful one)—if you're looking for a reference REST API implementation please note that this is, by design, not one. 

## Running the application

### Prerequisites

In order to run the application you'll need `node` and `npm`. The application was developed against `node v10.3.0` and `npm 6.1.0`—different versions may or may not work properly.

### Running the server
- open the `server` project using `cd server`
- install the dependencies using `npm install`
- build and serve the application using `npm start`

### Running the client
- open the `client` project using `cd client`
- install the dependencies using `npm install`
- build and serve the application using `npm start`