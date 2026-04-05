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

export interface FoodItemDto {
  name: string;
  categ: string;
  ingreds: string[];
  allergens: string[];
  tags: string[];
  rating: number;
}

export interface FAQ_el {
  hText: string;
  cont: string;
}

export interface StarProps {
    max?: number;
    value?: number;
    onChange?: (value: number) => void;
};