//
//  AppboyTracker.m
//  AMC
//
//  Created by Ignacio Mazzola on 7/8/15.
//  Copyright (c) 2015 VMBC. All rights reserved.
//

#import "AppboyTracker.h"
#import "DGTPTicketType.h"

@interface AppboyTracker ()
{
    Appboy *appBoyInstance;
}

@end


@implementation AppboyTracker

- (id)initWithAppBoyInstance:(Appboy *)theAppBoyInstance {
    self = [super init];
    if (self) {
        appBoyInstance = theAppBoyInstance;
    }
    
    return self;
}


- (void)trackEvent:(NSString *)event {
    if (![AppboyManager getInstance].enableAppboy)
        return;
    
    [appBoyInstance logCustomEvent:event];
}


- (void)trackEvent:(NSString *)event withAttributes:(NSDictionary *)attibs {
    if (![AppboyManager getInstance].enableAppboy)
        return;
    
    [appBoyInstance logCustomEvent:event withProperties:attibs];
}

- (void)trackPurchaseForOrder:(DGTPCreateOrder*)theOrder forShowtime:(DGTPShowtimeInfo*)stInfo withTickets:(NSArray*)tickets {
 
    for (DGTPTicketType *tType in theOrder.splittedTickets) {
        NSString *productId = [self getFriendlyProducType:tType.sku];
        NSDecimalNumber *total = (NSDecimalNumber*)theOrder.TotalCost;

        NSMutableDictionary *properties = [[NSMutableDictionary alloc] init];
        if ([productId isEqualToString:@"ticket"]) {
            properties = [self getPropertiesForTicketPurchase:theOrder forShowtime:stInfo ticketType:tType];
            [appBoyInstance logPurchase:productId inCurrency:@"USD" atPrice:total withProperties:properties];
        }
    }
}

- (NSString *)getFriendlyProducType:(NSString *)sku {
    NSRange firstSeparator = [sku rangeOfString:@"-"];
    if (firstSeparator.location != NSNotFound) {
        NSString *firstPart = [sku substringToIndex:firstSeparator.location];
        
        return [firstPart lowercaseString];
    }
    
    return @"";
}

- (NSMutableDictionary *)getPropertiesForTicketPurchase:(DGTPCreateOrder*)theOrder forShowtime:(DGTPShowtimeInfo*)stInfo ticketType:(DGTPTicketType *)tType {
    NSMutableDictionary *properties = [[NSMutableDictionary alloc] init];
    [properties setObject:stInfo.movie.title forKey:@"movie_name"];
    [properties setObject:tType.friendlyName forKey:@"ticket_type"];
    [properties setObject:@(1) forKey:@"ticket_qty"];
    [properties setObject:tType.unitPrice forKey:@"ticket_amnt"];
    [properties setObject:theOrder.ConvenienceFee forKey:@"convenience_fee"];
    
    return properties;
    
}



@end
