public static class CameraSensitivityConfig
{
    public static float normalizedSensitivity = 0.5f; // valor entre 0.1 e 1
    public static float maxXSpeedFactor = 600f;
    public static float maxYSpeedFactor = 3f;
    public static float sensititivityAim = 0.5f;

    public static float GetXSpeed() => normalizedSensitivity * maxXSpeedFactor;
    public static float GetYSpeed() => normalizedSensitivity * maxYSpeedFactor;
    public static float GetAimSensi() => sensititivityAim;
}
