namespace Flexy.UI
{
	public class FPSMeter : MonoBehaviour
	{
		private Single			_avarageFps;
		private Queue<Single>	_qLastFpsSmall	= new Queue<Single>(30);
		private Queue<Single>	_qLastFpsLong	= new Queue<Single>(60);

		[Range(1,30)]
		[SerializeField]	Byte		_qCountSmall	= 5;
		[Range(1,60)]
		[SerializeField]	Byte		_qCountLong;

		[Bindable]
		public				Int32		FPS			=> Mathf.Clamp(Mathf.CeilToInt(_avarageFps), 0, 100 );

		private				void		Update		( )						
		{
			_qLastFpsSmall	.Enqueue	( 1f / Time.unscaledDeltaTime );
			for (int i = 0; i < _qLastFpsSmall.Count - _qCountSmall; i++)
				_qLastFpsSmall.Dequeue(  );
			
			_qLastFpsLong	.Enqueue	( 1f / Time.unscaledDeltaTime );
			for (int i = 0; i < _qLastFpsLong.Count - _qCountLong; i++)
				_qLastFpsLong.Dequeue(  );

			//LINQ Average allocates so use custom Average func
			_avarageFps = Mathf.Max( GetAverage( _qLastFpsLong ), GetAverage( _qLastFpsSmall ) );

			//Analytics.FPS.Collect( FPS );
		}
		private				Single		GetAverage	( Queue<Single> q )		
		{
			var num1 = 0.0;
			var num2 = 0;
			foreach (var num3 in q)
			{
				num1 += num3;
				++num2;
			}
			if (num2 > 0L)
				return (Single) num1 / num2;
			return 0;
		}
	}
}
