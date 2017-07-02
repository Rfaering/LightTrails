using Assets.Models;
public class EffectOptions
{
    public static Effect[] Options =
    {
          Effect.Create("Plex",Effect.EffectKind.Particle, typeof(PlexMenuItem), loop: true),
          Effect.Create("Fireworks",Effect.EffectKind.Particle, typeof(FireworksMenuItem)),
          Effect.Create("Snow", Effect.EffectKind.Particle, typeof(SnowMenuItem)),
          Effect.Create("Distortion", Effect.EffectKind.Shader, typeof(DistortionMenuItem)),
          Effect.Create("Water", Effect.EffectKind.Shader, typeof(WaterMenuItem)),
          Effect.Create("Mist", Effect.EffectKind.Shader, typeof(MistMenuItem)),
          Effect.Create("SpeedLines", Effect.EffectKind.Shader),
          Effect.Create("Hay", Effect.EffectKind.Shader),
          Effect.Create("Wisps",Effect.EffectKind.Particle, typeof(WispMenuItem), loop: true),
          Effect.Create("Rain",Effect.EffectKind.Particle),
          Effect.Create("SmallFire", Effect.EffectKind.Particle, typeof(SmallFireMenuItem), loop: true),
          Effect.Create("Trails", Effect.EffectKind.Particle, typeof(TrailsMenuItem)),
          Effect.Create("Fire", Effect.EffectKind.Particle,typeof(FireMenuItem)),
          Effect.Create("LargeFire", Effect.EffectKind.Particle),
          Effect.Create("Focus", Effect.EffectKind.Particle, typeof(FocusMenuItem)),
          Effect.Create("Dream",Effect.EffectKind.Particle),
          Effect.Create("StarDust",Effect.EffectKind.Particle, typeof(StartDustMenuItem)),
          Effect.Create("Smoke", Effect.EffectKind.Particle),
          Effect.Create("Hearts",Effect.EffectKind.Particle, typeof(HeartsMenuItem), loop: true),
          Effect.Create("Bars", Effect.EffectKind.Shader, typeof(BarsMenuItem)),
          Effect.Create("Explosions", Effect.EffectKind.Particle,typeof(ExplosionMenuItem)),
          Effect.Create("Wave", Effect.EffectKind.Particle, typeof(WaveMenuItem), loop: true),
          Effect.Create("FadingStars", Effect.EffectKind.Particle, loop: true),
          Effect.Create("Lightning", Effect.EffectKind.Particle, typeof(LightningMenuItem)),
          Effect.Create("FlameThrower", Effect.EffectKind.Particle),
          Effect.Create("Flare", Effect.EffectKind.Particle)
};
}
