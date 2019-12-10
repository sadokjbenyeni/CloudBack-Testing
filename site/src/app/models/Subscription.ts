import { Billing } from "./Billing";
import { Term } from "./Terms";
import { SubscriptionType } from "./SubsriptionType";

export class Subscription {
    id: string;
    AccountId: string;
    Subscriber: string;
    Billing: Billing;
    cgv: Term;
    SubscriptionType: SubscriptionType;

    constructor(subscriptionType: SubscriptionType) {
        this.Billing = new Billing()
        this.cgv = new Term();
        this.SubscriptionType = subscriptionType
        
    }
}
