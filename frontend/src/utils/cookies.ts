export function getCookie(name: string): string | null {
  if (typeof document === "undefined") return null;
  const value = `; ${document.cookie}`;
  const parts = value.split(`; ${name}=`);
  if (parts.length < 2) return null;
  return parts.pop()!.split(";").shift() ?? null;
}

export function setCookie(
  name: string,
  value: string,
  opts?: { maxAgeSeconds?: number }
) {
  if (typeof document === "undefined") return;
  const maxAge = opts?.maxAgeSeconds
    ? `; max-age=${opts.maxAgeSeconds}`
    : "";
  document.cookie = `${name}=${encodeURIComponent(value)}; path=/; samesite=lax${maxAge}`;
}

export function deleteCookie(name: string) {
  if (typeof document === "undefined") return;
  document.cookie = `${name}=; path=/; max-age=0; samesite=lax`;
}

