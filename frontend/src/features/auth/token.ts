import { deleteCookie, getCookie, setCookie } from "@/utils/cookies";

const ACCESS_TOKEN_KEY = "access_token";

export function getAccessToken(): string | null {
  // Prefer cookie so Next middleware can see it; fall back to localStorage for safety.
  const cookieToken = getCookie(ACCESS_TOKEN_KEY);
  if (cookieToken) return decodeURIComponent(cookieToken);

  if (typeof window === "undefined") return null;
  return window.localStorage.getItem(ACCESS_TOKEN_KEY);
}

export function setAccessToken(token: string, maxAgeSeconds = 60 * 60) {
  if (typeof window !== "undefined") {
    window.localStorage.setItem(ACCESS_TOKEN_KEY, token);
  }
  setCookie(ACCESS_TOKEN_KEY, token, { maxAgeSeconds });
}

export function clearAccessToken() {
  if (typeof window !== "undefined") {
    window.localStorage.removeItem(ACCESS_TOKEN_KEY);
  }
  deleteCookie(ACCESS_TOKEN_KEY);
}

