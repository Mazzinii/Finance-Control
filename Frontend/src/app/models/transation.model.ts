export interface Transation {
  id?: string;
  description: string;
  status: string;
  value: number;
  personId: string;
  date?: Date;
}
