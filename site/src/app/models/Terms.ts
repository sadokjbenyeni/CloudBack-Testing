
export interface Terms {
  terms: Term[];
}

export interface Term {
  _id: string;
  id: { type: String };
  name: { type: String };
  description: string;
  version: string;
}
