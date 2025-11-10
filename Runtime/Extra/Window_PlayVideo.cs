using UnityEngine.Video;

namespace Flexy.UI
{
	public class Window_PlayVideo : State
	{
		public String	Url;
		public Boolean	PlayOnce = true;
		public Boolean	SkipByAnyKey = true;

		private Boolean IsVideoWasPlayed						
		{
			get => PlayerPrefs.GetInt( "Flexy.Boot.IsVideoWasPlayed", 0 ) != 0;
			set => PlayerPrefs.SetInt( "Flexy.Boot.IsVideoWasPlayed", value ? 1 : 0 );
		}

		private VideoPlayer _player;

		protected override void OnSetOpenParams(Object openData)
		{
			Url = openData as String ?? Url;
		}
		protected override void OnShow()
		{
			PlayVideoAsync().Forget();
		}

		private async UniTask PlayVideoAsync( )
		{
			if ( !String.IsNullOrEmpty( Url ) )
			{
				if ( Camera.main && (!IsVideoWasPlayed || !PlayOnce ))
				{
					Debug.Log			( $"[PlayVideoStep] - Playing video {Url}..." );
					IsVideoWasPlayed = true;

					var camera = Camera.main;
					_player = camera.gameObject.AddComponent<VideoPlayer>(  );

					_player.source       = VideoSource.Url;
					_player.url         = Url;
					_player.targetCamera = camera;
					_player.renderMode   = VideoRenderMode.CameraNearPlane;
					_player.Play(  );

					await UniTask.WaitWhile( ( ) => !_player.isPrepared || _player.isPlaying );

					_player.Stop();
					Destroy( _player );
					
					//wait destroy
					await UniTask.NextFrame(  );
				}
			}
			
			Close( );
		}

		private void Update( )
		{
			if( SkipByAnyKey && (Input.anyKeyDown || Input.touchCount > 0) && _player && _player.isPlaying )
				_player.Stop(  );
		}

		private void OnValidate( )
		{
			if( String.IsNullOrEmpty( Url ) )
				Debug.LogError( $"[PlayVideoStep] Video URL is not defined" );
		}
		
	}
}