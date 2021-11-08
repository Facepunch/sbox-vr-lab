using Sandbox;
using Sandbox.UI;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab
{
	public partial class LabPawn : BaseLabPawn
	{
		[ConVar.ClientData( "lab_toolmode" )]
		public string ToolMode { get; set; } = "create";

		[Net]
		public List<Entity> Selected { get; set; }

		public override void Simulate( Client cl )
		{
			base.Simulate( cl );


			if ( !Input.VR.IsActive ) return;

			DebugDrawHand( "Left", Input.VR.LeftHand );
			DebugDrawHand( "Right", Input.VR.RightHand );


			foreach ( var tracked in Input.VR.TrackedObjects )
			{
				DebugOverlay.Text( tracked.Transform.Position, $"Tracking: {tracked.Type}" );
			}


		}

		private void DebugDrawTracked( Input.TrackedObject obj )
		{
			DebugOverlay.Box( obj.Transform.Position, obj.Transform.Rotation, -0.9f, 0.9f, Color.White, 0.0f, true );
			DebugOverlay.Text( obj.Transform.Position, $"Type: {obj.Type}", 0.0f );
		}

		private void DebugDrawHand( string v, Input.VrHand hand )
		{
			DebugOverlay.Box( hand.Transform.Position, hand.Transform.Rotation, -1, 1, Color.Blue, 0.0f, true );
			DebugOverlay.Text( hand.Transform.Position, v, 0.0f );
		}
	}

}
