using Flexy.GameFlow;

namespace Flexy.UI
{
	public class UIWindow : State
	{
		// public override TransitionData GetTransition(State other, ETransition transition)
		// {
		// 	return default;
		// 	
		// 	//return new TransitionData( ExecuteTransition );
		// }
		
		public async UniTask ExecuteTransition(State prevState, State nextState, Boolean nextWasShown, Int32 uid, Object openData )
		{
			// var prevAnimData	= prevState ? prevState.GetHideAnimData( isFwdHide, nextState ) : default;
			// var nextAnimData	= nextState ? nextState.GetShowAnimData( isBackShow, prevState ) : default;
			// var allowCrossAnim	= nextState != prevState & !nextAnimData.DenyOverlay & !prevAnimData.DenyOverlay;
			
			var prevDt			= prevState?.GameStage;
			var nextDt			= nextState?.GameStage;
			var differentDt		= nextDt != prevDt;
			
			// if ( !nextState )
			// {
			// 	prevState.AnimateHide( isFwdHide, nextState );
			// 	await UniTask.Delay( TimeSpan.FromSeconds( prevAnimData.Duration ), DelayType.UnscaledDeltaTime );
			// 	prevState.gameObject.SetActive( false );
			// 	StateHide ( prevState, isMoveForward );
			// 	if( differentDt ) HideGameStage( prevState.GameStage );
			// }
			// else if ( !prevState )
			// {
			// 	if( differentDt ) ShowGameStage( nextState.GameStage );
			// 	StateShow( nextState, nextNode, isMoveForward, nextWasShown );
			// 	nextState.gameObject.SetActive( true );
			// 	nextState.AnimateShow( isBackShow, prevState );
			// 	await UniTask.Delay( TimeSpan.FromSeconds( nextAnimData.Duration - prevAnimData.Duration ), DelayType.UnscaledDeltaTime );
			// }
			// else if ( allowCrossAnim & nextAnimData.Duration >= prevAnimData.Duration )
			// {
			// 	if( differentDt ) ShowGameStage( nextState.GameStage );
			// 	StateShow( nextState, nextNode, isMoveForward, nextWasShown );
			// 	nextState.gameObject.SetActive( true );
			// 	nextState.transform.SetAsLastSibling( );
			// 	nextState.AnimateShow( isBackShow, prevState );
			// 	await UniTask.Delay( TimeSpan.FromSeconds( nextAnimData.Duration - prevAnimData.Duration ), DelayType.UnscaledDeltaTime );
			//
			// 	prevState.AnimateHide( isFwdHide, nextState );
			// 	await UniTask.Delay( TimeSpan.FromSeconds( prevAnimData.Duration ), DelayType.UnscaledDeltaTime );
			// 	prevState.gameObject.SetActive( false );
			// 	StateHide ( prevState, isMoveForward );
			// 	if( differentDt ) HideGameStage( prevState.GameStage );
			// }
			// else if ( allowCrossAnim & nextAnimData.Duration < prevAnimData.Duration )
			// {
			// 	prevState.AnimateHide( isFwdHide, nextState );
			// 	await UniTask.Delay( TimeSpan.FromSeconds( prevAnimData.Duration - nextAnimData.Duration ), DelayType.UnscaledDeltaTime );
			//
			// 	if( differentDt ) ShowGameStage( nextState.GameStage );
			// 	StateShow( nextState, nextNode, isMoveForward, nextWasShown );
			// 	nextState.gameObject.SetActive( true );
			// 	nextState.transform.SetAsLastSibling( );
			// 	nextState.AnimateShow( isBackShow, prevState );
			// 	await UniTask.Delay( TimeSpan.FromSeconds( nextAnimData.Duration ), DelayType.UnscaledDeltaTime );
			//
			// 	prevState.gameObject.SetActive( false );
			// 	StateHide ( prevState, isMoveForward );
			// 	if( differentDt ) HideGameStage( prevState.GameStage );
			// }
			// else
			// {
			// 	prevState.AnimateHide( isFwdHide, nextState );
			// 	await UniTask.Delay( TimeSpan.FromSeconds( prevAnimData.Duration ), DelayType.UnscaledDeltaTime );
			//
			// 	if( differentDt ) ShowGameStage( nextState.GameStage );
			// 	StateShow( nextState, nextNode, isMoveForward, nextWasShown );
			// 	nextState.gameObject.SetActive( true );
			// 	nextState.transform.SetAsLastSibling( );
			//
			// 	nextState.AnimateShow( isBackShow, prevState );
			// 	await UniTask.Delay( TimeSpan.FromSeconds( nextAnimData.Duration ), DelayType.UnscaledDeltaTime );
			//
			// 	prevState.gameObject.SetActive( false );
			// 	StateHide ( prevState, isMoveForward );
			// 	if( differentDt ) HideGameStage( nextState.GameStage );
			// }
		}
	}
}