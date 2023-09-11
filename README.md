# Storage.Service
 
 ## Intro
 This project was done to prove to myself that I could implement some kind of a Service with a basic purpose, using rather not-so-simple patterns.
 The purpose of this Service is to store... Strings. One at a time.
 It can be expanded to support any kind of context, from simple to complex (ints? Personnel information? Books catalog?).

 ## How it works
 There's a `Storage.Repositories` project in the solution that connects to a local instance of MSSQL Server, and stores data there, and is also capable of retrieving it, either by ID or _en mass_, using a filter to include/exclude objects that have been marked as deleted.
 
 ## Domain

 ### StringDTO
 Currently, everything revolves around _Strings_; we save a String value, assign it an ID (int), take note of the timestamp of insertion on the `CreatedAt` property, and when updated, it will also update the `LastModifiedAt` property.
 For auditoring purposes, we'll mark an entity as `Deleted` by setting a timestamp to its `DeletedAt` property instead of actually removing it from the DB table.

 ## Relevant .csprojs

 ### Storage.Service
 This service only has a few endpoints:
 - Create `String`: POST to `/Storage/Strings` with values encoded in the request's body as XML or JSON
	- Will return a `200` (OK) with the `StringDTO` object created
	- Can return a `400` (BadRequest) error if provided with an empty or whitespace value
	- Can return a `409` (Conflict) error if something went wrong in the underlying layers
 - Get `String` by its `identifier`: `GET` to `/Storage/Strings/{id}`
	- Will return a `200` (OK) with the `StringDTO` result in the response's body as XML or JSON
	- Can throw a `400` (BadRequest) error if provided with an ID below 1
 - Get all `String` objects: `GET` to `/Storage/Strings/all/{includeDeleted:bool}` 
	- Will return a `200` (OK) with a list of `StringDTO` results, including those that have been marked as `Deleted` IF the `includeDeleted` value is `true`
	- Will return a `404` (NotFound) if something went wrong in the underlying layers

### Storage.Client
This is the Http-based Client for submitting requests to the `Storage.Service`'s `StorageController`.
TODO: generate & publish NuGet package to a custom feed upon release.

## Tests

### Unit Tests

At the moment, only the `Storage.Client` has a relevant level of code coverage.
Work will be done in the near future to cover most of the code-base (other projects), so v1.0.0 can be released.

### Integration Tests

None so far.

### Analysis Tools

None so far; considering SonarQube integration (possibly with GitHub as well)