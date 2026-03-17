import { API_BASE_URL } from "@/constants/env";
import type { AccessTokenResponse } from "@/types/auth";
import { fetchJson } from "./http";

export async function login(username: string, password: string) {
  return fetchJson<AccessTokenResponse>(`${API_BASE_URL}/api/auth/login`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ username, password }),
  });
}

export async function register(username: string, password: string) {
  const res = await fetch(`${API_BASE_URL}/api/auth/register`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ username, password }),
  });
  if (!res.ok) {
    const text = await res.text().catch(() => "");
    throw new Error(text || `Register failed (${res.status})`);
  }
}

