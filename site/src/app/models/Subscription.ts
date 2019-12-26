import { Billing } from "./Billing";
import { Term } from "./Terms";
import { SubscriptionType } from "./SubsriptionType";
import { Payment } from "./Payment";

export class Subscription {
    accountId: string;
    subscriber: string;
    billing: Billing;
    cgv: Term;
    paymentCard: Payment;
    subscriptionType: SubscriptionType;

    constructor(subscriptionType: SubscriptionType) {
        this.billing = new Billing()
        this.cgv = new Term();
        this.subscriptionType = subscriptionType

    }
}
