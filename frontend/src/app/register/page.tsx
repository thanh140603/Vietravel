"use client";

import { useState } from "react";
import { useRouter } from "next/navigation";
import styles from "@/styles/app.module.css";
import { register } from "@/services/auth";

export default function RegisterPage() {
  const router = useRouter();

  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  async function onSubmit(e: React.FormEvent) {
    e.preventDefault();
    setLoading(true);
    setError(null);

    try {
      await register(username, password);
      router.replace("/login");
    } catch (e) {
      setError(e instanceof Error ? e.message : String(e));
    } finally {
      setLoading(false);
    }
  }

  return (
    <div className={styles.page}>
      <main className={`${styles.main} ${styles.authMain}`}>
        <header className={`${styles.header} ${styles.authHeader}`}>
          <h1 className={styles.title}>Register</h1>
        </header>

        <div className={`${styles.card} ${styles.authCard}`} style={{ padding: 16 }}>
          <form onSubmit={onSubmit} style={{ display: "grid", gap: 12 }}>
            <label style={{ display: "grid", gap: 6 }}>
              <span style={{ opacity: 0.75, fontSize: 13 }}>Username</span>
              <input
                value={username}
                onChange={(e) => setUsername(e.target.value)}
                placeholder="username"
                autoComplete="username"
                style={inputStyle}
              />
            </label>

            <label style={{ display: "grid", gap: 6 }}>
              <span style={{ opacity: 0.75, fontSize: 13 }}>Password</span>
              <input
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                placeholder="password"
                type="password"
                autoComplete="new-password"
                style={inputStyle}
              />
            </label>

            <button type="submit" disabled={loading} style={buttonStyle}>
              {loading ? "Creating..." : "Create account"}
            </button>

            <div style={{ fontSize: 13, opacity: 0.78, textAlign: "center" }}>
              Already have an account?{" "}
              <a href="/login" style={{ opacity: 1 }}>
                Sign in
              </a>
            </div>
          </form>
        </div>

        {error ? (
          <div className={styles.error}>
            <div className={styles.errorTitle}>Register failed</div>
            <pre className={styles.errorBody}>{error}</pre>
          </div>
        ) : null}

      </main>
    </div>
  );
}

const inputStyle: React.CSSProperties = {
  padding: "10px 12px",
  borderRadius: 12,
  border: "1px solid rgba(255,255,255,0.14)",
  background: "rgba(255,255,255,0.06)",
  color: "rgba(255,255,255,0.92)",
  outline: "none",
};

const buttonStyle: React.CSSProperties = {
  padding: "10px 12px",
  borderRadius: 12,
  border: "1px solid rgba(255,255,255,0.18)",
  background: "rgba(255,255,255,0.12)",
  color: "rgba(255,255,255,0.92)",
  cursor: "pointer",
  fontWeight: 600,
};

