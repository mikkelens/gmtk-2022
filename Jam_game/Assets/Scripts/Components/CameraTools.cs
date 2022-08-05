using System.Diagnostics.CodeAnalysis;
using Tools;
using UnityEngine;

namespace Components
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
	public static class CameraTools
	{
	#region Caching
		// camera itself
		private static Camera _main;
		private static Camera Main
		{
			get
			{
				_main ??= Camera.main;
				if (_main == null) Debug.LogWarning("No main camera found.");
				return _main;
			}
		}
		
		// camera transform
		private static Transform _transform;
		private static Transform Transform => _transform ??= Main.transform;
		
		// camera controller
		private static CameraController _controller;
		private static CameraController Controller
		{
			get
			{
				if (_controller != null) return _controller;
				_controller = CameraController.Instance;
				if (_controller == null) Debug.LogWarning("No CameraController found.");
				return _controller;
			}
		}
		
		// calculated values
		private static float _previousFOV;
		private static float _previousAspect;
		private static float _previousAngle;
		private static Vector3 _previousPosition;
		private static Vector2 _visibleGroundFrustum;
		public static Vector2 VisibleGroundFrustum
		{
			get
			{
				if (Main.fieldOfView == _previousFOV && Main.aspect == _previousAspect && _previousAngle == Transform.eulerAngles.x) return _visibleGroundFrustum;
				_previousFOV = Main.fieldOfView;
				_previousAspect = Main.aspect;
				_previousAngle = Transform.eulerAngles.x;
				float distance = DistanceToGroundAtRotation();
				float frustumHeight = CalculateFrustumHeightAtDistance(distance);
				float frustumWidth = frustumHeight * Main.aspect;
				return _visibleGroundFrustum = new Vector2(frustumWidth, frustumHeight);
			}
		}
	#endregion

	#region Math shorthands
		public static float Angle => Transform.eulerAngles.x;
		public static float HalfVerticalFOV => Main.fieldOfView * 0.5f; // assume field of view is vertical
		public static float AngleFromDown => 90 - Transform.eulerAngles.x;
		public static Vector2 OrtographicSize
		{
			get
			{
				float height = Main.orthographicSize;
				return new Vector2(height * Main.aspect, height);
			}
		}
	#endregion

	#region Camera position

		public static float AngleDistanceOffset()
		{
			float a = Transform.position.y;
			float c = DistanceToGroundAtRotation();
			return Mathf.Sqrt(c * c - a * a);
		}
		public static float DistanceToGroundAtRotation()
		{
			return Transform.position.y / Mathf.Cos(AngleFromDown * Mathf.Deg2Rad);
		}
		public static float CalculateFrustumHeightAtDistance(float depth)
		{
			return 2.0f * depth * Mathf.Tan(Main.fieldOfView * 0.5f * Mathf.Deg2Rad);
		}
		public static Vector2 PositionNoOffset => Transform.position.WorldToPlane() - Controller.Offset.WorldToPlane();
	#endregion
			
	#region Screen/Mouse conversions
		public static Vector2 ScreenPosToCenterPoint(this Vector2 screenPos)
		{
			return Main.ScreenToViewportPoint(screenPos) - Vector3.one * 0.5f;
		}
		public static Vector2 PositionToScreenPoint(this Vector3 position)
		{
			return Main.WorldToScreenPoint(position);
		}
		public static Vector2 ScreenToViewportPoint(this Vector2 screenPos)
		{
			return Main.ScreenToViewportPoint(screenPos);
		}
	#endregion
	}
}