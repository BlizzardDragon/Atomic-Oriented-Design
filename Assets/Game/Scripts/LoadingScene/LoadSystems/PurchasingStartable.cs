// using Purchasing;
// using Services;
// using Unity.Services.Core;

// namespace AtomicOrientedDesign.Shooter
// {
//     public sealed class PurchasingStartable : IAppStartable
//     {
//         private PurchaseManager purchaseManager;

//         [ServiceInject]
//         public void Construct(PurchaseManager purchaseManager)
//         {
//             this.purchaseManager = purchaseManager;
//         }

//         async void IAppStartable.Start()
//         {
//             await UnityServices.InitializeAsync();
//             this.purchaseManager.Initialize();
//         }
//     }
// }