using Assets.UI.Models;
public class EffectOptions
{
    public static Effect[] Options =
    {
          Effect.Create("Fireworks",Effect.EffectKind.Particle, typeof(FireworksMenuItem)),
          Effect.Create("Snow", Effect.EffectKind.Particle, typeof(SnowMenuItem)),
          Effect.Create("Distortion", Effect.EffectKind.Shader, typeof(DistortionMenuItem)),
          Effect.Create("Wisps",Effect.EffectKind.Particle, typeof(WispMenuItem), loop: true),
          Effect.Create("Rain",Effect.EffectKind.Particle),
          Effect.Create("SmallFire", Effect.EffectKind.Particle, typeof(SmallFireMenuItem), loop: true),
          Effect.Create("Trails", Effect.EffectKind.Particle, typeof(TrailsMenuItem), loop: true),
          Effect.Create("Fire", Effect.EffectKind.Particle,typeof(FireMenuItem)),
          Effect.Create("LargeFire", Effect.EffectKind.Particle),
          Effect.Create("Focus", Effect.EffectKind.Particle, typeof(FocusMenuItem)),
          Effect.Create("Dream",Effect.EffectKind.Particle),
          Effect.Create("Twinkles",Effect.EffectKind.Particle, typeof(TwinklesMenuItem)),
          Effect.Create("Smoke", Effect.EffectKind.Particle),
          Effect.Create("Hearts",Effect.EffectKind.Particle, typeof(HeartsMenuItem), loop: true),
          Effect.Create("Bars", Effect.EffectKind.Shader, typeof(BarsMenuItem)),
          Effect.Create("Explosions", Effect.EffectKind.Particle,typeof(ExplosionMenuItem)),
          Effect.Create("Wave", Effect.EffectKind.Particle, typeof(WaveMenuItem), loop: true)
};
}
