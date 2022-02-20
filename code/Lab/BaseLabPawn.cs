using Sandbox;
using Sandbox.UI;

namespace Lab
{
	public partial class BaseLabPawn : Sandbox.AnimEntity
	{
		public override void Spawn()
		{
			base.Spawn();

			Tags.Add( "player", "pawn" );
			SetModel( "models/light_arrow.vmdl" );
		}

		public override void Simulate( Client cl )
		{
			base.Simulate( cl );

			Rotation = Input.Rotation;
			EyeRotation = Rotation;

			DoMovement( cl );
		}

		public virtual void DoMovement( Client cl )
		{
			var maxSpeed = 500;
			if ( Input.Down( InputButton.Run ) ) maxSpeed = 1000;
			if ( Input.Down( InputButton.Duck ) ) maxSpeed = 100;

			var WishVelocity = Vector3.Zero;
			WishVelocity += Input.VR.LeftHand.Joystick.Value.y * Input.VR.Head.Rotation.Forward;
			WishVelocity += Input.VR.LeftHand.Joystick.Value.x * Input.VR.Head.Rotation.Right;

			Velocity = Velocity.AddClamped( WishVelocity * maxSpeed * 5 * Time.Delta, maxSpeed );
			Velocity = Velocity.Approach( 0, Time.Delta * maxSpeed * 3 );

			//
			// Move helper traces and slides along surfaces for us
			//
			MoveHelper helper = new MoveHelper( Position, Velocity );
			helper.Trace = helper.Trace.Size( 20 );

			helper.TryUnstuck();
			helper.TryMoveWithStep( Time.Delta, 30.0f );

			Position = helper.Position;
			Velocity = helper.Velocity;
			EyePosition = Position;
		}

		public override void FrameSimulate( Client cl )
		{
			base.FrameSimulate( cl );

			Rotation = Input.Rotation;
			EyeRotation = Rotation;
			Position += Velocity * Time.Delta;
		}
	}

}
