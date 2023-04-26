## Architecture

* I didn't use interface with all the service classes because I see all of the service classes belonging to one logical unit of business and it makes little sense to mock them out. This is why most of the tests actually deal with `SpendService`, due to the fact that is the place where use cases start from.
* I did use interfaces for repository classes so I can return fake data.
* External SDK is hidden behind an interface so we don't have a direct dependency on external code.
* External service and failover have another class behind which they are hidden (`ExternalSupplierInvoiceService`). This class also should use Polly with a Circuit Breaker to handle resilience, but I didn't manage to get to it.
* `FailoverInvoiceService` uses `FailoverInvoiceOptions` to know where is the cutoff for stale data. If it encounters stale data it throws.
* A UML diagram is attached describing classes.

## Unit tests

* Tests rely heavily on various builders to manage complexity of arranging a test case.
* I ran a test coverage analysis and have included the results below. Repositories haven't been covered at all, but those would need integration tests (Sqlite, EF Core in-memory provider or even Testcontainers).
![2023-04-27 01_35_39-CodingTest - Microsoft Visual Studio](https://user-images.githubusercontent.com/26722936/234726963-3e6f5eaf-71b1-4a61-ba67-d9e91c6095fc.png)


## Resilience

* Resilience should be implemented with Polly. Unfortunately I have not managed to properly implement it, but I do have two proposals which I would use to get the conversation going. More on that can be found in `ExternalSupplierInvoiceService.cs`.
