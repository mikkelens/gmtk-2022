using System;
using System.Diagnostics.CodeAnalysis;
using Tools;
using UnityEngine;

namespace Level
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
				_controller ??= CameraController.Instance;
				if (_controller == null) Debug.LogWarning("No CameraController found.");
				return _controller;
			}
		}
		
		// calculated values
		private static float _previousFOV;
		private static Vector3 _previousPosition;
		private static Vector2 _visibleGroundFrustum;
		public static Vector2 VisibleGroundFrustum // todo: use this
		{
			get
			{
				if (Math.Abs(Main.fieldOfView - _previousFOV) < 0.1f) return _visibleGroundFrustum;
				float downAngle = AngleFromDown;
				float angleOffset = HalfVerticalFOV;
				float upperFrustumHeight = CalculateFrustumHeightAtDistance(DistanceToGroundAtAngle(downAngle + angleOffset));
				float lowerFrustumHeight = CalculateFrustumHeightAtDistance(DistanceToGroundAtAngle(downAngle - angleOffset));
				float combinedFrustumHeight = upperFrustumHeight + lowerFrustumHeight;
				float frustumWidth = combinedFrustumHeight * Main.aspect;
				return _visibleGroundFrustum = new Vector2(upperFrustumHeight, frustumWidth);
			}
		}
	#endregion

	#region Math shorthands
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
		public static float DistanceToGroundAtAngle(float angle)
		{
			return Transform.position.y / Mathf.Cos(angle * Mathf.Deg2Rad);
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