
export interface Country {
    id: { type: String };
    name: { type: String};
    ue: { type: String};
    rgx: { type: String };
  }
  
  export interface Countries {
    countries: Country [];
  }
  