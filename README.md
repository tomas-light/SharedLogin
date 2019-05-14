# Readme

How to setup for Asp.Net Core:

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddMvc();
    
    // other code
    
    SharedLoginConfiguration.Configure(services, Configuration, Configuration.GetConnectionString("MyDbConnection"));    
}
```

![module scheme](https://lh3.googleusercontent.com/xRXJm_2zcALE0Gs2He2YLeAPjDzr3SDkt7Xl0lWU0PDIxPObch56uU5xMaHxh032TRVF7Mh0MekIaxtkHojhng1dRbTrAeFqNv37D0n6scaXa_hY4qxw9d_U_0bt65jFQc1b6jT5vVuH_93SChgnEq3G6GOJeteRWDvHgTWrRnr7Tdr-H7Oa5rgLOjLmCALcK8OQ_PPDLshoOLUex1m8LKJyt3XNx3ZyOW8-BjqCWatjC7S-5V324Q6ApB7HLqRwaRntH0HWXeTuD7YO2Tz4A_lDxOmEiTndMixcN-kwvIJz4u_2B7b7webgJLBaViMROIXh8PU_j4HvFcZbf7M6RasfZvQ_9BB7zXKJCuUzHRjbVQPvL2x1UX-QzTa7fsvfhTZ_U4G6h_nY3lBe0QK2L8sFOSYiTxGxCsoW0Ice1UcnXaZfc_x2T18QFtHAvmRZSbTL80JbwYxPSZKmqJIFAr4_X1pf3P-oCKqIhheSw69bqLlhDX7-_CwU2IOaXLcCQ6D8uoGA0U_wRbThHf0mjtDWX7J1P-bh2WQjiVLZ6rcMS60EPjWmBAgJQMO9tuOEi7C4-i96lS4CS8D66p6YIRBWHk3G8vsyLJDoFmQ=w1920-h969)
