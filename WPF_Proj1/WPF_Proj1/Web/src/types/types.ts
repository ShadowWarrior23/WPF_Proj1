export interface UserDto {
  id: number;
  username: string;
  fullName: string;
  balance: number;
}

export interface OrderDto {
  id: number;
  userId: number;
  day: string;
  wantsSoup: boolean;
  dishChoice: string | null;
}

export interface DailyMenuDto {
  day: string;
  soup: string;
  dishA: string;
  dishB: string;
}