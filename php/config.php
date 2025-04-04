<?php

class Config
{
    private const DBHOST = "localhost";
    private const DBUSER = "root";
    private const DBPASS = "";
    private const DBNAME = "restful";

    private $dsn = "mysql:host=" . self::DBHOST . ";dbname=" . self::DBNAME . "";
    protected $conn = null;

    public function __construct()
    {
        try {
            $this->conn = new PDO($this->dsn, self::DBUSER, self::DBPASS);
            $this->conn->setAttribute(PDO::ATTR_DEFAULT_FETCH_MODE, PDO::FETCH_ASSOC);
        } catch (PDOException $e) {
            die("Connection failed: " . $e->getMessage());
        }

        return $this->conn;
    }

    function secureInput($data)
    {
        if (is_array($data)) {
            return array_map('secureInput', $data);
        }

        $data = trim($data);
        $data = stripslashes($data);
        $data = htmlspecialchars($data, ENT_QUOTES, 'UTF-8');
        $data = strip_tags($data);

        return $data;
    }

    public function message($content, $status)
    {
        return json_encode(['message' => $content, 'code' => $status]);
    }
}
