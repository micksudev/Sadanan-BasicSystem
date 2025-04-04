<?php

class Jwt
{
    private const SECRETKEY = '8ade26264c1fc047120cf9a7d6599464a8376ddc0fc3e12f30036a639ef43e3f';

    public static function jwt_encode($payload) {
        $header = [
            'alg' => 'HS256',
            'typ' => 'JWT'
        ];
        $encodedHeader = self::base64url_encode(json_encode($header));
        $encodedPayload = self::base64url_encode(json_encode($payload));
        $signature = hash_hmac('sha256', $encodedHeader . '.' . $encodedPayload, self::SECRETKEY, true);
        $encodedSignature = self::base64url_encode($signature);
        $jwt = $encodedHeader . '.' . $encodedPayload . '.' . $encodedSignature;
        return $jwt;
    }

    public static function jwt_decode($jwt) {
        $parts = explode('.', $jwt);
        if (count($parts) !== 3) {
            return false;
        }
        list($header, $payload, $signature) = $parts;
        $expectedSignature = hash_hmac('sha256', $header . '.' . $payload, self::SECRETKEY, true);
        $decodedSignature = self::base64url_decode($signature);

        if (hash_equals($decodedSignature, $expectedSignature)) {
            return json_decode(self::base64url_decode($payload), true);
        } else {
            return false;
        }
    }

    private static function base64url_encode($data) {
        return str_replace(['+', '/', '='], ['-', '_', ''], base64_encode($data));
    }

    private static function base64url_decode($data) {
        $data = str_replace(['-', '_'], ['+', '/'], $data);
        $padding = strlen($data) % 4;
        if ($padding) {
            $data .= str_repeat('=', 4 - $padding);
        }
        return base64_decode($data);
    }
}
