using Godot;
using System;

public partial class SquareWave : Node
{
	private AudioStreamPlayer _player;
	
	private float SampleHz = 16000f;
	[Export]
	public int CurrentNote { get; set; }
	[Export(PropertyHint.Range, "0.0,1.0,")]
	public float Volume { get; set; } = 1f;
	private float _phase = 0f;
	
	private AudioStreamGeneratorPlayback _playback;
	
	public override void _Ready()
	{
		_player = GetNode("AudioStreamPlayer") as AudioStreamPlayer;
		((AudioStreamGenerator)_player.Stream).MixRate = SampleHz;
		_player.Play();
		_playback = (AudioStreamGeneratorPlayback)_player.GetStreamPlayback();
		FillBuffer();
	}
	
	public override void _Process(double delta)
	{
		FillBuffer();
	}
	
	public float GetFrequency()
	{
		return Mathf.Pow(2f, (float)CurrentNote/12f) * 440f;
	}
	
	public void FillBuffer()
	{
		float increment = GetFrequency()/SampleHz;
		int toFill = _playback.GetFramesAvailable();
		
		while (toFill > 0)
		{
			_playback.PushFrame(Vector2.One * Mathf.Sign(Mathf.Sin(_phase * Mathf.Tau)) * Volume);
			_phase = (_phase + increment) % 1f;
			toFill -= 1;
		}
	}
}
