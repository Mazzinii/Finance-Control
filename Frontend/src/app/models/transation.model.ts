export interface Transation {
  id?: string;
  description: string;
  status: string;
  value: number;
  date?: string;
  userGuid: string;
}
