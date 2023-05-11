# Coinbase API application Backend

# What is this?
- This is the backend to application I am creating that will use all of coinbases 3rd party API's for real time market data, as well as an attempt to make it easier for newer traders to interface with when it comes to putting in stop losses or buy orders

- This is also my first attempt at a clean ASP.NET architechture so this will be a learning process for me.

# Usage
- More to come later. However this is not intended for use by others

# Changelog

### 5/11/2023 
* The magic click happened where I finally understand clean architecture so the speed at which I create this will happen much quicker (Thank Lord)
* Updated many services, created the Subscription service that was necessary to continuously receive market data from the coinbase API.
* MarketDataController was created and is ready for testing
* Note: We cannot generate a mass amount of market data at once without declaring "Currency Pairs" of all Individual coin subscriptions within the MarketDataSubscription custom service
* Remodeled the AppUser Entity, Removed UserId (We will just be Using EF.Core inherited primary key ID)
* Remodeled all entities to user the AppUser as foreign key and navigation property
* WatchlistEntity, Service, and interface are created and are ready to be tested. The controller for handling a users watchlist still needs to be created