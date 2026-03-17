"use client";

import styles from "@/styles/app.module.css";
import { useEffect, useMemo, useState } from "react";
import { useRouter } from "next/navigation";
import { getTours } from "@/services/tours";
import { clearAccessToken, getAccessToken } from "@/features/auth/token";

type Tour = {
  id: number;
  name: string;
  price: number;
  city: string;
};

export default function Home() {
  const apiBaseUrl = useMemo(
    () => process.env.NEXT_PUBLIC_API_BASE_URL ?? "http://localhost:5141",
    []
  );
  const router = useRouter();

  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [tours, setTours] = useState<Tour[]>([]);
  const [reloadKey, setReloadKey] = useState(0);

  useEffect(() => {
    let cancelled = false;

    async function run() {
      try {
        setLoading(true);
        setError(null);

        const token = getAccessToken();
        if (!token) {
          router.replace("/login");
          return;
        }

        const data = await getTours(token);
        if (!cancelled) setTours(data);
      } catch (e) {
        const msg = e instanceof Error ? e.message : String(e);
        if (msg.includes("401") || msg.toLowerCase().includes("unauthorized")) {
          clearAccessToken();
          router.replace("/login");
          return;
        }
        if (!cancelled) setError(e instanceof Error ? e.message : String(e));
      } finally {
        if (!cancelled) setLoading(false);
      }
    }

    run();
    return () => {
      cancelled = true;
    };
  }, [apiBaseUrl, router, reloadKey]);

  function logout() {
    clearAccessToken();
    router.replace("/login");
  }

  const currency = useMemo(
    () =>
      new Intl.NumberFormat("vi-VN", {
        style: "currency",
        currency: "VND",
        maximumFractionDigits: 0,
      }),
    []
  );

  return (
    <div className={styles.page}>
      <main className={styles.main}>
        <header className={`${styles.header} ${styles.toolbar}`}>
          <div>
            <h1 className={styles.title}>Tours</h1>
            <div className={styles.subtitle}>
              Danh sách tour (Name / Price / City)
            </div>
          </div>
          <div className={styles.actions}>
            <button className={styles.btn} onClick={() => setReloadKey((x) => x + 1)}>
              Refresh
            </button>
            <button className={styles.btn} onClick={logout}>
              Logout
            </button>
          </div>
        </header>

        {loading ? (
          <div className={styles.state}>Loading...</div>
        ) : error ? (
          <div className={styles.error}>
            <div className={styles.errorTitle}>Failed to load tours</div>
            <pre className={styles.errorBody}>{error}</pre>
          </div>
        ) : tours.length === 0 ? (
          <div className={styles.card} style={{ padding: 16 }}>
            <div style={{ opacity: 0.8, fontWeight: 650 }}>Chưa có tour</div>
            <div style={{ opacity: 0.7, marginTop: 6 }}>
              Hãy chạy `db/seed_tours.sql` để thêm dữ liệu mẫu.
            </div>
          </div>
        ) : (
          <div className={styles.list}>
            {tours.map((t) => (
              <div key={t.id} className={styles.tourCard}>
                <div>
                  <div className={styles.tourName}>{t.name}</div>
                  <div className={styles.tourMeta}>
                    <span className={styles.badge}>{t.city}</span>
                    <span>•</span>
                    <span>Mã tour: #{t.id}</span>
                  </div>
                </div>
                <div>
                  <div className={styles.price}>{currency.format(t.price)}</div>
                  <div className={styles.priceNote}>/ khách</div>
                </div>
              </div>
            ))}
          </div>
        )}
      </main>
    </div>
  );
}
