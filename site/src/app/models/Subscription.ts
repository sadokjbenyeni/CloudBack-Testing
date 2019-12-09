import { Billing } from "./Billing";
import { Term } from "./Terms";
import { SubscriptionType } from "./SubsriptionType";

export class Subscription {
    Id: string
    AccounId: string
    Billing: Billing
    cgv: Term
    SubScriptionType: SubscriptionType;

    constructor(subscriptionType:SubscriptionType) {
        this.Billing = new Billing()
        this.cgv = new Term();
        this.SubScriptionType=subscriptionType
    }
}
