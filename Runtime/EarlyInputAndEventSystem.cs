using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Profiling;

namespace Flexy.UI
{
	public class EarlyInputAndEventSystem : EventSystem
	{
		private readonly	CustomSampler	_samplerEarlyInputAndEventSystem= CustomSampler.Create("EarlyInputAndEventSystem");
		private readonly	CustomSampler	_samplerInputSystem				= CustomSampler.Create("InputSystem");
		private readonly	CustomSampler	_samplerEventSystem				= CustomSampler.Create("EventSystem");

		protected override	void	OnEnable		( )	
		{
			PreUpdateLoop().Forget();
			InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsManually;
			base.OnEnable();
		}
		protected override	void	Update			( ) { }
		private async		UniTask	PreUpdateLoop	( )	
		{
			while (enabled)
			{
				await UniTask.NextFrame(PlayerLoopTiming.LastEarlyUpdate);
			
				_samplerEarlyInputAndEventSystem.Begin();
				{
					_samplerInputSystem.Begin();
					InputSystem.Update();
					_samplerInputSystem.End();
				
					_samplerEventSystem.Begin();
					base.Update();
					_samplerEventSystem.End();
				}
				_samplerEarlyInputAndEventSystem.End();
			}
		}
	}
}