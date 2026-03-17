import { API_BASE_URL } from "@/constants/env";
import type { Tour } from "@/types/tour";
import { fetchJson } from "./http";

export async function getTours(accessToken: string) {
  return fetchJson<Tour[]>(`${API_BASE_URL}/api/tours`, {
    headers: { Authorization: `Bearer ${accessToken}` },
  });
}

