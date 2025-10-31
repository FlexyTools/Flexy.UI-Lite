using UnityEngine.Video;

namespace Flexy.UI.Extra
{
	public class Window_PlayVideo : State
	{
		public String?	Url;
		public Boolean	PlayOnce = true;
		public Boolean	SkipByAnyKey = true;

		private Boolean IsVideoWasPlayed						
		{
			get => PlayerPrefs.GetInt( "Flexy.Boot.IsVideoWasPlayed", 0 ) != 0;
			set => PlayerPrefs.SetInt( "Flexy.Boot.IsVideoWasPlayed", value ? 1 : 0 );
		}

		private VideoPlayer? _player;

		protected override void OnShow			( )		
		{
			PlayVideoAsync().Forget();
		}

		private async	UniTask	PlayVideoAsync	( )		
		{
			var url = OpenParams as String ?? Url;
		
			if (!String.IsNullOrEmpty(url))
			{
				if (Camera.main && (!IsVideoWasPlayed || !PlayOnce))
				{
					Debug.Log		( $"[Window_PlayVideo] - Playing video {url}..." );
					IsVideoWasPlayed = true;

					var camera = Camera.main;
					_player = camera.gameObject.AddComponent<VideoPlayer>();

					_player.source       = VideoSource.Url;
					_player.url         = url;
					_player.targetCamera = camera;
					_player.renderMode   = VideoRenderMode.CameraNearPlane;
					_player.Play();

					await UniTask.WaitWhile(() => !_player.isPrepared || _player.isPlaying);

					_player.Stop();
					Destroy(_player);
					_player = null;
					
					//wait destroy
					await UniTask.NextFrame();
				}
			}
			
			Close();
		}

		private			void	Update			( )		
		{
			if (SkipByAnyKey && (Input.anyKeyDown || Input.touchCount > 0) && _player && _player!.isPlaying)
				_player.Stop();
		}
		private			void	OnValidate		( )		
		{
			if (String.IsNullOrEmpty(Url))
				Debug.LogError( $"[Window_PlayVideo] Video URL is not defined" );
		}
	}
}