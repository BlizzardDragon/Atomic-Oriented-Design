// using System.Threading.Tasks;
// using Cysharp.Threading.Tasks;
// using UnityEngine;

// namespace AtomicOrientedDesign.Shooter
// {
//     [CreateAssetMenu(
//         fileName = "LoadingTask_LoadPopups",
//         menuName = "Configs/Tasks/New LoadingTask_LoadPopups"
//     )]
//     public sealed class LoadingTask_LoadPopups : LoadingTask
//     {
//         public async override UniTask<Result> Do()
//         {
//             var popupCatalog = Resources.Load<PopupCatalog>(nameof(PopupCatalog));
//             await popupCatalog.LoadAssets();

//             return await Task.FromResult(new Result
//             {
//                 Success = true
//             });
//         }
//     }
// }