﻿namespace SoundManager.Endpoints.Sound;

public record GetSoundEffectByIdResponse(Guid Id, string Name, int TotalMilliseconds, int Offset, int PlayDuration, double VolumePercent);