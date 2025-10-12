namespace Flexy.UI
{
	[RequireComponent(typeof(Canvas))]
	[DisallowMultipleComponent]
	public class AutoCanvasScaler : CanvasScaler
	{
		private Canvas _canvas;

		protected override void OnEnable()
		{
			_canvas = GetComponent<Canvas>();
			base.OnEnable();
		}

		protected override void Handle()
		{
			if (_canvas == null || !_canvas.isRootCanvas)
				return;
		
			if (m_ScreenMatchMode == ScreenMatchMode.Expand)
			{
				var screenSize = _canvas.renderingDisplaySize;

				// Multiple display support only when not the main display. For display 0 the reported
				// resolution is always the desktops resolution since its part of the display API,
				// so we use the standard none multiple display method.
				var displayIndex = _canvas.targetDisplay;
				if (displayIndex > 0 && displayIndex < Display.displays.Length)
				{
					var disp = Display.displays[displayIndex];
					screenSize = new Vector2(disp.renderingWidth, disp.renderingHeight);
				}
				
				if (Math.Sign(screenSize.x - screenSize.y) != Math.Sign(referenceResolution.x - referenceResolution.y))
					referenceResolution = new Vector2(referenceResolution.y, referenceResolution.x);
			}
				
			base.Handle();
		}
	}
}